.PHONY: all

GetScripts = $(shell grep -E "^@[ \t]+$1[ \t]+module" scripts.cfg | awk '{print $$4".fos"}')

ASCOMPILER = LD_LIBRARY_PATH=$$LD_LIBRARY_PATH:../libs ./ASCompiler

DIR_TOP ?= ../..
DIR_OUT  = $(DIR_TOP)/.githooks/cache/pre-commit.scripts

CLIENT_MODULES = $(addprefix $(DIR_OUT)/,$(call GetScripts,client))
MAPPER_MODULES = $(addprefix $(DIR_OUT)/,$(call GetScripts,mapper))
SERVER_MODULES = $(addprefix $(DIR_OUT)/,$(call GetScripts,server))

MODULES = \
    $(CLIENT_MODULES:.fos=.client.fosp) \
    $(MAPPER_MODULES:.fos=.mapper.fosp) \
    $(SERVER_MODULES:.fos=.server.fosp)

all: $(DIR_OUT) $(MODULES)

$(DIR_OUT):
	mkdir -p $@

$(DIR_OUT)/%.client.fosp: %.fos
	@$(ASCOMPILER) $< -p $@ -client

$(DIR_OUT)/%.mapper.fosp: %.fos
	@$(ASCOMPILER) $< -p $@ -mapper

$(DIR_OUT)/%.server.fosp: %.fos
	@$(ASCOMPILER) $< -p $@

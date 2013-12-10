DIR_OUT = Release
EXTENSIONS = \
    parameters \
    client_parameters \
    check_look \
    Utils \
    MapperUtils \
    online_stats \
    DevConnect

all:
	@for dir in $(EXTENSIONS); do $(MAKE) -C $$dir; done

clean:
	@for dir in $(EXTENSIONS); do $(MAKE) -C $$dir $@; done
	rm -rf $(DIR_OUT)

again: clean all

copy: all
	cp $(addprefix $(DIR_OUT)/,$(addsuffix .so,$(EXTENSIONS))) ../scripts

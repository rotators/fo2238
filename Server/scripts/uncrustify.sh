#!/bin/bash

for f in *.fos; do
    uncrustify -c uncrustify.cfg --no-backup $f
done
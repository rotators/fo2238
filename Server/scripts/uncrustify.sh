#!/bin/bash

args=$*

for f in *.fos; do
	if [ $f == "critical_table.fos" ]; then 
		continue;
	fi
	uncrustify -c uncrustify.cfg --no-backup $args $f
done

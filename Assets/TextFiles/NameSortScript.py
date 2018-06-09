#!/usr/bin/python

namesFile = open("MaleFirstNames.txt", "r+")

names = []

for(i = 0; i < 900; i++)
	name = namesFile.readline()
	names.append(name)

namesFile.close()
names.sort()

namesFile = open("MaleFirstNames.txt", "w")
for(i = 0; i < 900; i++)
	namesFile.writeline(names[i])
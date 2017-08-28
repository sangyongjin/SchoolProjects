#!/usr/bin/env python
#Python 2.7
#Assignment 1 - RBAC system
#Daniel Navarro, uhohitdanny@csu.fullerton.edu
#Sangyong Jin, sangyongjin@csu.fullerton.edu
import sys
import string 

def check(groupfile, resources, attempts):
	
	name = ""
	action = ""
	#reads file
	lines = []
	#used when splitting or stripping lines[] to form new strings
	strings = []
	#will create group associated with name of each person
	groupcompare = []
	#defaulted as DENY, if permission is verified it will be flipped to 1.
	allowbit = 0
	
	##for resources file##
	#first adds the object, then the group:perm pairs in the same list
	group_perms = []
	#a condensed list containing group_perms list
	resourcelist = []

	##for attempts file##
	attempt = []

	##for groupfile##
	group_user = []

	#first we handled the attempts and group files
	#add them too arrays accordingly
	with open(attempts) as a:
		for line in a:
			strings = line.split()
			attempt.append(strings)
	
	with open(groupfile) as g:
		for line in g:
			strings = line.rstrip('\n')
			strings = strings.replace(',','').replace(':','')
			strings = strings.split()
			group_user.append(strings)

	#next we handled the more complicated file read by
	#making a list that associates the objec with group:perms appropiately
	#then add that list to another list so that we can condense the data
	with open(resources) as r:
		for line in r:
			if (line == '\n'):
				resourcelist.append(group_perms)
				group_perms = []
				pass
			else:
				strings = line.strip('\t').rstrip('\n')
				#indicates that it's a new object/file
				if strings[0] == "/":
					#reads object/directory string
					strings = strings.rstrip(':')
					#add object as first element of list
					group_perms.append(strings)
				else:
					#reads the following groups:permissions associated with that group
                       			strings = strings.replace(',','').replace(':','')
                        		strings = strings.split()
					#add the group:permissions to the rest of the list following the obj in the first index
					group_perms.append(strings)
		#this is for when we reach EOF 
		resourcelist.append(group_perms)
	#############
	#COMPARISON#
	############
	#read attempt array and do comparisons#
	for x in attempt:
		name = x[0]
		action = x[1]
		for y in resourcelist:
			#match the obj in attempt to resourcelist's object
			if x[2] == y[0]:
				#check which group the name is in
				for group in group_user:
					if name in group:
						#save group associated with name
						groupcompare.append(group[0])
				#use groupcompare list and compare with resourcelist's object file
				for gc in groupcompare:
					for index in y:
						if gc in index:
							#check if action is in that group ACL
							if action in index:
								allowbit = 1
				#clear groupcompare list
				groupcompare = []
		#DENY default 
		if(allowbit == 0):
			print "DENY", ' '.join(map(str,x))
		else:
			print "ALLOW", ' '.join(map(str,x))
			#reset allowbit
			allowbit = 0
				#if groupcompare list does not contain same group as any of theresourcelist's group 
				#then no permission so by default permission is DENIED
				#for anything.  The logic is if there is a group match check then check if action is in the list

		
if __name__ == "__main__":
	
	check(sys.argv[1], sys.argv[2], sys.argv[3])

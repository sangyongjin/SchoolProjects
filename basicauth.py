#/usr/bin/env python
from scapy.all import *
import base64

keyphrase = "Authorization: Basic"

def summarize(pkt):
	#data is held in the Raw section
	#check if there is content contained in the packet
	if pkt.haslayer(Raw):
		s= ""
		s += str(pkt.getlayer(TCP))
		# check if the phrase we are looking for is in string array
		if keyphrase in s:
			#get the index of where the keyphrase begins
			pos = s.index(keyphrase)		
			#adjust the index to start before the encoded data
			pos = pos + len(keyphrase)
			#save the string from the index, cuts down the string size
			#encoded data is in the beginning of the string
			s = s[pos:]
			#after encoded data, there should be a newline char.
			#get the index of the newline and save string up to that index
			#which would just be the encoded data
			pos = s.index('\n')	
			s = s[:pos].strip()
			#after stripping the newline, decode the base64 encoded data
			s = base64.b64decode(s)
			print "WARNING: Unprotected credentials! " + s	
#only connect to port that http connects to
sniff(prn=summarize, filter="tcp port 80", store=0)


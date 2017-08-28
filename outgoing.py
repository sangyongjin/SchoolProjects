#/usr/bin/env python
import socket

#function returns IP addr of this machine
def get_ip():
	s = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)
	s.connect(("8.8.8.8", 80))
	return s.getsockname()[0]

#resolves ipaddr to a hostname, defaults to ipaddr if no hostname
def resolve_hostname(ipaddr):
	try:
		return socket.gethostbyaddr(ipaddr)[0]
	except socket.herror:
		return ipaddr
	

from scapy.all import *

ip = get_ip()
dst = []
seen = []

def summarize(pkt):
	if (pkt.haslayer(IP)):
		#keep track of all IP dst addr it sees
		if pkt.getlayer(IP).dst not in dst:
				dst.append(pkt.getlayer(IP).dst)
	
		#checking each packet that is TCP and if src addr = our ip addr
		if (pkt.haslayer(TCP)) and (pkt.getlayer(IP).src == ip):
			#check if ip dst addr is seen before
			if pkt.getlayer(IP).dst not in seen:
				#if not seen print hostname, if no hostname can be resolved print ip addr
				print resolve_hostname(pkt.getlayer(IP).dst)	
				#after printed, save dst addr into seen
				seen.append(pkt.getlayer(IP).dst)

sniff(prn=summarize, store=0)


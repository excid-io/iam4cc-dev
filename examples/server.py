import socket
import sys

address = 'localhost'
port    = 6002
sock    = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
server  = (address, port )
sock.bind(server)
sock.listen(1)
connection, client_address = sock.accept()
data = connection.recv(1024)
print ("Received ", data)
connection.sendall(data)
connection.close()
sock.close()
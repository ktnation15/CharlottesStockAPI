import socket
import time
import random
import json

def send_broadcast_message():
    # Opret en UDP socket
    udp_socket = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)
    # Tillad broadcast
    udp_socket.setsockopt(socket.SOL_SOCKET, socket.SO_BROADCAST, 1)
    # Definer broadcast IP og port
    broadcast_ip = '255.255.255.255'
    port = 14014
    # Definer produkt nummer
    product_no = 8013

    try:
        while True:
            amount_sold = random.randint(1, 10)
            message = json.dumps({'productNo': product_no, 'sold': amount_sold})
            
            udp_socket.sendto(message.encode('utf-8'), (broadcast_ip, port))
            print(f"Sent: {message}")
            
            # Vent et tilfældigt antal sekunder mellem 1 og 3
            time.sleep(random.uniform(1, 3))
    except KeyboardInterrupt:
        print("Stopping the broadcast sender")
    finally:
        udp_socket.close()
if __name__ == "__main__":
    send_broadcast_message()

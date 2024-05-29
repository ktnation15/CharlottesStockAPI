import socket
import json
import requests
import time

def start_udp_server():
    udp_ip = "0.0.0.0"
    udp_port = 14014
    
    # Opret en UDP socket
    udp_socket = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)
    udp_socket.bind((udp_ip, udp_port))
    
    print(f"Listening for UDP messages on port {udp_port}...")
    
    while True:
        data, addr = udp_socket.recvfrom(1024)  # Buffer størrelse er 1024 bytes
        message = data.decode('utf-8')
        print(f"Received message: {message} from {addr}")
        
        process_message(message)

def process_message(message):
    try:
        # Parse JSON-beskeden
        data = json.loads(message)
        product_no = data['productNo']
        amount_sold = data['sold']
        
        print(f"Processing productNo: {product_no}, amountSold: {amount_sold}")
        
        # Opdater lagerniveau via CharlottesStockAPI
        update_stock(product_no, amount_sold)
    except (json.JSONDecodeError, KeyError) as e:
        print(f"Failed to process message: {message}. Error: {e}")

def update_stock(product_no, amount_sold):
    api_url = "http://localhost:5000/api/EasterEggs"
    
    # Hent det aktuelle lager for produktet
    response = requests.get(f"{api_url}/{product_no}")
    if response.status_code == 200:
        egg = response.json()
        egg['InStock'] -= amount_sold  # Træk det solgte antal fra lageret
        
        # Send en HTTP PUT-request for at opdatere produktet
        update_response = requests.put(api_url, json=egg)
        if update_response.status_code == 200:
            print(f"Successfully updated product {product_no} with new stock level: {egg['InStock']}")
        else:
            print(f"Failed to update product {product_no}. Status code: {update_response.status_code}")
    else:
        print(f"Failed to fetch product {product_no}. Status code: {response.status_code}")

if __name__ == "__main__":
    start_udp_server()

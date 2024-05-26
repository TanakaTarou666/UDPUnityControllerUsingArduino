#include <SPI.h>
#include <WiFiS3.h>
#include <WiFiUdp.h>

#include "arduino_secrets.h"
char ssid[] = SECRET_SSID;
char pass[] = SECRET_PASS;

WiFiUDP Udp;
unsigned int localPort = 8892;  //unityで設定したローカルポ�EチEIPAddress destinationIP(127,0,0,1); // wifiのipアドレス

void setup() {
  Serial.begin(9600);
  // WiFiに接綁E  WiFi.begin(ssid, pass);
  while (WiFi.status() != WL_CONNECTED) {
    delay(500);
    Serial.print(".");
  }
  Serial.println("\nWiFi connected");
  
  // IPアドレスを取得し表示
  IPAddress ip = WiFi.localIP();
  Serial.print("IP Address: ");
  Serial.println(ip);

  Udp.begin(localPort);
}

void loop() {
  // チE�Eタの送信
  byte dataToSend = 0b00000010; //末尾の010が右、E00が左
  Udp.beginPacket(dataToSend, localPort); // 送信先�EIPアドレスとポ�Eトを持E��E  Udp.write(dataToSend);
  Udp.endPacket();
  Serial.println("send messeage");

  delay(1000); // 1秒征E��
}

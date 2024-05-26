#include <SPI.h>
#include <WiFiS3.h>
#include <WiFiUdp.h>

#include "arduino_secrets.h"
char ssid[] = SECRET_SSID;
char pass[] = SECRET_PASS;

WiFiUDP Udp;
unsigned int localPort = 8892;  //unity縺ｧ險ｭ螳壹＠縺溘Ο繝ｼ繧ｫ繝ｫ繝昴・繝・IPAddress destinationIP(127,0,0,1); // wifi縺ｮip繧｢繝峨Ξ繧ｹ

void setup() {
  Serial.begin(9600);
  // WiFi縺ｫ謗･邯・  WiFi.begin(ssid, pass);
  while (WiFi.status() != WL_CONNECTED) {
    delay(500);
    Serial.print(".");
  }
  Serial.println("\nWiFi connected");
  
  // IP繧｢繝峨Ξ繧ｹ繧貞叙蠕励＠陦ｨ遉ｺ
  IPAddress ip = WiFi.localIP();
  Serial.print("IP Address: ");
  Serial.println(ip);

  Udp.begin(localPort);
}

void loop() {
  // 繝・・繧ｿ縺ｮ騾∽ｿ｡
  byte dataToSend = 0b00000010; //譛ｫ蟆ｾ縺ｮ010縺悟承縲・00縺悟ｷｦ
  Udp.beginPacket(dataToSend, localPort); // 騾∽ｿ｡蜈医・IP繧｢繝峨Ξ繧ｹ縺ｨ繝昴・繝医ｒ謖・ｮ・  Udp.write(dataToSend);
  Udp.endPacket();
  Serial.println("send messeage");

  delay(1000); // 1遘貞ｾ・▽
}

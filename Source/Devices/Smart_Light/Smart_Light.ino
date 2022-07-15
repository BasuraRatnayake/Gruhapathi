/*
  Smart Light v2 
  Copyright 2017 Basura Ratnayake. All rights reserved.
*/

#include <VirtualWire.h>
const int relay_pin = 12;
const int led_pin = 6;
const int receive_pin = 7;
const int transmit_pin = 8;
const int voltage_pin = A1;
const int pir_pin = A2;

const String mySerial = "LIG10";
const String myHub = "HUB21";

int getMaxValue();
float calculateVoltage();
uint8_t converToArray(String);

float voltage = 0;
void setup() {
  pinMode(relay_pin, OUTPUT);
  pinMode(led_pin, OUTPUT);
  pinMode(voltage_pin, INPUT);
  pinMode(pir_pin, INPUT);

  vw_set_ptt_inverted(true); // Required for DR3100
  vw_set_rx_pin(receive_pin);
  vw_set_tx_pin(transmit_pin);
  vw_setup(4000);
  Serial.begin(9600);

  vw_rx_start();

  digitalWrite(relay_pin, LOW);
  digitalWrite(led_pin, LOW);
}

String message = "";
bool power_state = false;

void loop() {  
  uint8_t buf[VW_MAX_MESSAGE_LEN];
  uint8_t buflen = VW_MAX_MESSAGE_LEN;

  if (vw_get_message(buf, &buflen)) {
    for (int i = 0; i < buflen; i++) {
      message += (char)buf[i];
    }
    Serial.println("rtdgh");

    String serial = message.substring(0, 5);
    String command = "";
    if (serial == mySerial) {
      command = message.substring(6);
      if (command == "GETV") {
        command = myHub+"|"+mySerial+"|OFF";
        uint8_t s = converToArray(command);
        vw_send((uint8_t *)s, strlen(s));
        vw_wait_tx(); 
        Serial.println(command);
        delay(1000); 
      } else if (command == "ON") {
        digitalWrite(relay_pin, HIGH);
        digitalWrite(led_pin, HIGH);
        power_state = true;
        Serial.println("V1");
      } else if (command == "OFF") {
        digitalWrite(relay_pin, LOW);
        digitalWrite(led_pin, LOW);
        power_state = false;
        Serial.println("V2");
      }
    }
    message = "";
    serial = "";
    command = "";
  }
  
  voltage = calculateVoltage();  

  int sensorValue = digitalRead(pir_pin);
  if (sensorValue == 1) {
    digitalWrite(relay_pin, HIGH);
    digitalWrite(led_pin, HIGH);
    delay(7000);
  }

  if(power_state == false){
    digitalWrite(relay_pin, LOW);
    digitalWrite(led_pin, LOW);
  }  
  delay(1000);
}

uint8_t converToArray(String dataString){
  uint8_t dataArray[dataString.length()];
  dataString.toCharArray(dataArray, dataString.length());
  return dataArray;
}

int getMaxValue() {
  int sensorValue;    //value read from the sensor
  int sensorMax = 0;
  uint32_t start_time = millis();
  while ((millis() - start_time) < 1000) {
    sensorValue = analogRead(voltage_pin);
    if (sensorValue > sensorMax) {
      sensorMax = sensorValue;
    }
  }
  return sensorMax;
}
float calculateVoltage() {
  float amplitude_current;      // Float amplitude current
  float effective_value;       // Float effective current

  int sensor_max;
  sensor_max = getMaxValue();

  amplitude_current = (float)(sensor_max - 512) / 1024 * 66 / 185 * 1000000;
  effective_value = amplitude_current / 1.414;
  return amplitude_current;
}


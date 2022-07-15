
#include <VirtualWire.h>
char *controller;
void setup() {
  pinMode(5, OUTPUT);
  pinMode(6, OUTPUT);
  pinMode(7, OUTPUT);
  pinMode(9, OUTPUT);
  
  vw_set_ptt_inverted(true); //
  vw_set_tx_pin(8);
  vw_setup(4000);// speed of data transfer Kbps
    
  digitalWrite(5, HIGH);
  delay(4000); 
  digitalWrite(6, HIGH);
}

void loop() {
  delay(1000); 
  controller = "LIG10|OF";
  //controller = "OFFON";
  vw_send((uint8_t *)controller, strlen(controller));
  digitalWrite(7, HIGH);
  digitalWrite(9, LOW);
  vw_wait_tx(); // Wait until the whole message is gone
  delay(1000); 
  digitalWrite(7, LOW);
  digitalWrite(9, HIGH);
  delay(2000); 
}

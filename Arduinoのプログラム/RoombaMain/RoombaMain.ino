#include <SPI.h>

#include <SoftwareSerial.h>;
SoftwareSerial device(10, 11);

int nowFunc = 0;//stop:0 forward:1 back:2 R:3 L:4
byte counter = 0;
void setup() {
  device.begin(115200);
  Serial.begin(115200);
}

void loop() {
  counter++;
  if (counter == 5)counter = 0;
  char inkey;
  char roombaSignal;
  if ( Serial.available() > 0 ) {//PCから何か信号が来た時の処理
    inkey = Serial.read();
    //Serial.print( inkey );

    switch (inkey) {
      case 'w': nowFunc = 1; break;
      case 'd': nowFunc = 3; break;
      case 'a': nowFunc = 4; break;
      case 's': nowFunc = 2; break;
      case 'n': nowFunc = 0; break;
      case 'v': virtualWallCall(); break;
      case 'r': roombaReset(); break;
      default : break;
    }
  }

  if (counter == 0) {

    switch (nowFunc) {
      case 1: moveForward(); break;
      case 3: moveRight(); break;
      case 4: moveLeft(); break;
      case 2: moveBack(); break;
      default : moveStop(); break;
    }
  }


  if (device.available() > 0 ) {//ルンバから何か信号が来た時の処理
    roombaSignal = device.read();
    //Serial.print("received data ->");
    //Serial.println(roombaSignal);
    if (roombaSignal == 1) {
      Serial.print("v");
      Serial.println(",");
      Serial.print("v");
      Serial.println(",");
    }
  }
  delay(1);
}

void startUp() {
  byte buffer[] = {
    byte(128), // Start
    byte(132), // FULL
  };
  device.write(buffer, 2);
}

void motor(int l, int r) {
  byte buffer[] = {
    byte(128), // Start
    byte(132), // FULL
    byte(146), // Drive PWM
    byte(r >> 8),
    byte(r),
    byte(l >> 8),
    byte(l)
  };
  device.write(buffer, 7);
}

void virtualWallCall() {
  byte buffer[] = {
    byte(142), // Sensors
    byte(13)//Virtual Wall
  };
  device.write(buffer, 2);
}

void roombaReset() {
  byte buffer[] = {
    byte(173), // Stop
  };
  device.write(buffer, 1);
}

void moveForward() {
  //Serial.println("F");
  motor(127, 127);
}

void moveBack() {
  //Serial.println("B");
  motor(-127, -127);
}
void moveRight() {
  //Serial.println("R");
  motor(127, -127);
}
void moveLeft() {
  //Serial.println("L");
  motor(-127, 127);
}
void moveStop() {
  //Serial.print("S");
  motor(0, 0);
}

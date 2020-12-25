#include <SPI.h>

#include <SoftwareSerial.h>;
SoftwareSerial device(10, 11);

int recentFunc = 0;//stop:0 other:1
byte counter = 0;
void setup() {
  device.begin(115200);
  Serial.begin(115200);
}

void loop() {
  counter++;
  if (counter == 50)counter = 0;
  char inkey;
  char roombaSignal;
  if (counter == 10) {
    if ( Serial.available() > 0 ) {//PCから何か信号が来た時の処理
      inkey = Serial.read();
      //Serial.print( inkey );

      switch (inkey) {
        case 'w': moveForward(); break;
        case 'd': moveRight(); break;
        case 'a': moveLeft(); break;
        case 's': moveBack(); break;
        case 'v': virtualWallCall(); break;
        case 'r': roombaReset(); break;
        default : break;
      }

    } else {
      moveStop();
    }
  }
  delay(1);
  if (device.available() > 0 ) {//ルンバから何か信号が来た時の処理
    roombaSignal = device.read();
    //Serial.print("received data ->");
    //Serial.println(roombaSignal);
    if (roombaSignal == 1) {
      Serial.print("v");
      Serial.println(",");
      Serial.print("v");
      Serial.println(",");
      delay(1);
    }
  }


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
    byte(146), // Drive PWM
    byte(r >> 8),
    byte(r),
    byte(l >> 8),
    byte(l)
  };
  device.write(buffer, 5);
}

void virtualWallCall() {
  byte buffer[] = {
    byte(142), // Sensors
    byte(13)//Virtual Wall
  };
  device.write(buffer, 2);
  Serial.print("v");
  Serial.println(",");
  delay(1);
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
  recentFunc = 1;

}

void moveBack() {
  //Serial.println("B");
  startUp();
  motor(-127, -127);
  recentFunc = 2;
}
void moveRight() {
  //Serial.println("R");
  motor(127, -127);
  recentFunc = 3;
}
void moveLeft() {
  //Serial.println("L");
  motor(-127, 127);
  recentFunc = 4;
}
void moveStop() {
  if (recentFunc == 0) {
    //Serial.print("S");
    motor(0, 0);
  }
  recentFunc = 0;
}

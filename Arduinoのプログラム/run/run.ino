bool isEnable;

void setup() {
  isEnable = true;
  Serial.begin(9600);
  pinMode(2, INPUT);
  pinMode(7, OUTPUT);
  pinMode(5, OUTPUT);
  Serial.write(0);
}

void loop() {
  if ( Serial.available() > 0 ) {//PCから何か信号が来た時の処理
    char inkey;
    inkey = Serial.read();
    Serial.write(inkey);
    //Serial.print( inkey );

    switch (inkey) {
      case '1': isEnable = false; break;
      case '0': isEnable = true; break;
      default : break;
    }
  }
  if (isEnable) {
    digitalWrite(7, HIGH);
  } else {
    digitalWrite(7, LOW);
  }
  /*
    int sw = analogRead(2);
    if (sw == HIGH) {
      Serial.write(1);
    }
    else if (sw == LOW) {
      Serial.write(0);
    }
  */
  delay(5);
}

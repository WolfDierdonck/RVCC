#include <AFMotor.h>

AF_DCMotor speedMotor(2, MOTOR12_1KHZ); 
AF_DCMotor turnMotor(3, MOTOR34_1KHZ);
char character;
String command;

void setup() {
  Serial.begin(9600);
}

void loop() {
  while (Serial.available()) {
    delay(20);
    character = Serial.read();
    command += character;
  }
  if (command.length()>0) {
    Serial.print("[");
    Serial.print(command);
    Serial.println("]");
    if (command == "forwards" || command == "go") {
      Serial.println(command);
      speedMotor.setSpeed(255);
      speedMotor.run(BACKWARD);
    }
    else if (command == "backwards") {
      Serial.println(command);
      speedMotor.setSpeed(255);
      speedMotor.run(FORWARD);
    }
    else if (command == "stop") {
      Serial.println(command);
      speedMotor.setSpeed(0);
      speedMotor.run(RELEASE);
    }
    else if (command == "left") {
      Serial.println(command);
      turnMotor.setSpeed(255);
      turnMotor.run(BACKWARD);
      delay(50);
      turnMotor.setSpeed(0);
      turnMotor.run(RELEASE);
    }
    else if (command == "go left") {
      Serial.println(command);
      speedMotor.setSpeed(255);
      speedMotor.run(BACKWARD);
      turnMotor.setSpeed(255);
      turnMotor.run(BACKWARD);
      delay(50);
      turnMotor.setSpeed(0);
      turnMotor.run(RELEASE);
    }
    else if (command == "right") {
      Serial.println(command);
      turnMotor.setSpeed(255);
      turnMotor.run(FORWARD);
      delay(50);
      turnMotor.setSpeed(0);
      turnMotor.run(RELEASE);
    }
    else if (command == "go right") {
      Serial.println(command);
      speedMotor.setSpeed(255);
      speedMotor.run(BACKWARD);
      turnMotor.setSpeed(255);
      turnMotor.run(FORWARD);
      delay(50);
      turnMotor.setSpeed(0);
      turnMotor.run(RELEASE);
    }
    command = "";
  }
}

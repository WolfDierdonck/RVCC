 char t;
String test;

int redPin = 2;
int bluePin = 3;
int greenPin = 4;
int yellowPin = 5;

void setup() {
  Serial.begin(9600);
  pinMode(redPin, OUTPUT);
  pinMode(bluePin, OUTPUT);
  pinMode(greenPin, OUTPUT);
  pinMode(yellowPin, OUTPUT);
}

void loop() {
  while (Serial.available()) {
    delay(20);
    t = Serial.read();
    test +=t;
  }
  if (test.length()>0) {
    Serial.println(test);
    if (test == "Left") {
      Serial.println("Pin 2 ON");
      digitalWrite(redPin, HIGH);
      delay(1000);
      digitalWrite(redPin, LOW);
    }
    else if (test == "Right") {
      Serial.println("Pin 3 ON");
      digitalWrite(bluePin, HIGH);
      delay(1000);
      digitalWrite(bluePin, LOW);
    }
    else if (test == "Stop") {
      Serial.println("Pin 4 ON");
      digitalWrite(greenPin, HIGH);
      delay(1000);
      digitalWrite(greenPin, LOW);
    }
    else if (test == "Go") {
      Serial.println("Pin 5 ON");
      digitalWrite(yellowPin, HIGH);
      delay(1000);
      digitalWrite(yellowPin, LOW);
    }
    test = "";
  }
  
  
}

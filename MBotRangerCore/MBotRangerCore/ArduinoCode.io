#include <Arduino.h>
#include <Wire.h>
#include <SPI.h>
#include <SoftwareSerial.h>
#include <MeAuriga.h>
MeRGBLed rgbled_0(0, 12);
MeWifi Wifi(PORT_10);
MeUltrasonicSensor ultrasonic_6(6);


//Encoder Motor
MeEncoderOnBoard Encoder_1(SLOT1);
MeEncoderOnBoard Encoder_2(SLOT2);
int input=0;
void isr_process_encoder1(void)
{
      if(digitalRead(Encoder_1.getPortB()) == 0){
            Encoder_1.pulsePosMinus();
      }else{
            Encoder_1.pulsePosPlus();
      }
}

void isr_process_encoder2(void)
{
      if(digitalRead(Encoder_2.getPortB()) == 0){
            Encoder_2.pulsePosMinus();
      }else{
            Encoder_2.pulsePosPlus();
      }
}

void move(int direction, int speed)
{
      int leftSpeed = 0;
      int rightSpeed = 0;
      if(direction == 1){
            leftSpeed = -speed;
            rightSpeed = speed;
      }else if(direction == 2){
            leftSpeed = speed;
            rightSpeed = -speed;
      }else if(direction == 3){
            leftSpeed = -speed;
            rightSpeed = -speed;
      }else if(direction == 4){
            leftSpeed = speed;
            rightSpeed = speed;
      }
      Encoder_1.setTarPWM(leftSpeed);
      Encoder_2.setTarPWM(rightSpeed);
}
void moveDegrees(int direction,long degrees, int speed_temp)
{
      speed_temp = abs(speed_temp);
      if(direction == 1)
      {
            Encoder_1.move(-degrees,(float)speed_temp);
            Encoder_2.move(degrees,(float)speed_temp);
      }
      else if(direction == 2)
      {
            Encoder_1.move(degrees,(float)speed_temp);
            Encoder_2.move(-degrees,(float)speed_temp);
      }
      else if(direction == 3)
      {
            Encoder_1.move(-degrees,(float)speed_temp);
            Encoder_2.move(-degrees,(float)speed_temp);
      }
      else if(direction == 4)
      {
            Encoder_1.move(degrees,(float)speed_temp);
            Encoder_2.move(degrees,(float)speed_temp);
      }
}

double angle_rad = PI/180.0;
double angle_deg = 180.0/PI;




void setup(){
    //Set Pwm 8KHz
    TCCR1A = _BV(WGM10);
    TCCR1B = _BV(CS11) | _BV(WGM12);
    TCCR2A = _BV(WGM21) | _BV(WGM20);
    TCCR2B = _BV(CS21);
    
     attachInterrupt(Encoder_1.getIntNum(), isr_process_encoder1, RISING);
    attachInterrupt(Encoder_2.getIntNum(), isr_process_encoder2, RISING);
   
 Serial.begin(9600); 
Wifi.begin(9600);

}


void loop(){
 

  _loop();
    Serial.available();
    input = Wifi.read();  // read serial 
     if((ultrasonic_6.distanceCm()) < (10)){
      if(input=='2'){
      move(2,100);        
      rgbled_0.setpin(44);
    rgbled_0.setColor(0,255,0,0);
    rgbled_0.show();
    _delay(2);
    }
      move(0,0);        
      rgbled_0.setpin(44);
    rgbled_0.setColor(0,0,0,0);
    rgbled_0.show();
     Serial.print(ultrasonic_6.distanceCm());
    
      }
    
    
   switch (input){
   case '1':  //If C# sent a '1' Move Forward
     {
      if((ultrasonic_6.distanceCm()) < (10)){
      move(0,0);        
      rgbled_0.setpin(44);
    rgbled_0.setColor(0,0,0,0);
    rgbled_0.show();
      break;
    }
       move(1,100);
            rgbled_0.setpin(44);
    rgbled_0.setColor(0,0,225,0);
    rgbled_0.show();
     
     }
      break;
   case '2':
     //If C# sent a '2' Move Backward
     {  
      move(2,100);        
      rgbled_0.setpin(44);
    rgbled_0.setColor(0,255,0,0);
    rgbled_0.show();
     }
   
      break;

       case '3':
     //If C# sent a '3' Move Left
     {  
      move(3,100);        
      rgbled_0.setpin(44);
    rgbled_0.setColor(0,255,255,0);
    rgbled_0.show();
     }
   
      break;
      case '4':
     //If C# sent a '4' Move Right
     {  
      move(4,100);        
      rgbled_0.setpin(44);
    rgbled_0.setColor(0,0,255,255);
    rgbled_0.show();
     }
   
      break;

      
      case '5':
     //If C# sent a '5' Stop
     {  
      move(0,0);        
      rgbled_0.setpin(44);
    rgbled_0.setColor(0,0,0,0);
    rgbled_0.show();
     }
   
      break;
 }
     
}

void _delay(float seconds){
    long endTime = millis() + seconds * 1000;
    while(millis() < endTime)_loop();
}

void _loop(){
  
    Encoder_1.loop();
    Encoder_2.loop();
    
}


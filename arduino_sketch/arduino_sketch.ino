#include <LiquidCrystal.h>
#include <SPI.h>
#include <aREST.h>
#include <avr/wdt.h>

const int rs = 8, en = 9, d4 = 4, d5 = 5, d6 = 6, d7 = 7;
LiquidCrystal lcd(rs, en, d4, d5, d6, d7);
String id = String(7);
String openedCommand = String(" opened");
String closedCommand = String(" closed");

int lcd_key     = 5;
int adc_key_in  = 0;
#define btnRIGHT  0
#define btnUP     1
#define btnDOWN   2
#define btnLEFT   3
#define btnSELECT 4
#define btnNONE   5

aREST rest = aREST();

unsigned long lastTimePrinted = 0;
 
int read_LCD_buttons()
{
 adc_key_in = analogRead(0);
 if (adc_key_in > 1500) return btnNONE; 
 if (adc_key_in < 50)   return btnRIGHT;  
 if (adc_key_in < 195)  return btnUP; 
 if (adc_key_in < 380)  return btnDOWN; 
 if (adc_key_in < 500)  return btnLEFT; 
 if (adc_key_in < 700)  return btnSELECT;   
 return btnNONE;
}

void setup() {
  Serial.begin(115200);
  lcd.begin(16, 2);
  lcd.print("Hello, stranger");
  lcd.setCursor(0, 1);
  rest.function("open", openLock);
  rest.function("close", closeLock);

  // Give name and ID to device (ID should be 6 characters long)
  rest.set_id("2");
  rest.set_name("serial");

  // Start watchdog
  wdt_enable(WDTO_4S);
}

void loop() {
  if (lastTimePrinted + 2000 < millis()) {
    lcd.setCursor(0, 1);
    lcd.print("      ");
  }
  lcd_key = read_LCD_buttons();
  switch(lcd_key) {
    case btnRIGHT:
    {
      openLock();
      break; 
    }
    case btnLEFT: {
      closeLock();
      break;
    }
    default: {
      break;
    }
  }
  rest.handle(Serial);
  wdt_reset();   
}

String openLock(){
  notifyOpening();
  lcd.setCursor(0, 1);
  lcd.print("Opened");
  lastTimePrinted = millis();
  return String("HTTP/1.1 200 OK\nContent-Length: 0\nConnection: Closed");
}

String closeLock() {
  notifyClosing();
  lcd.setCursor(0, 1);
  lcd.print("Closed");
  lastTimePrinted = millis();
  return 0;
}

void notifyOpening() {

}

void notifyClosing() {
  
}

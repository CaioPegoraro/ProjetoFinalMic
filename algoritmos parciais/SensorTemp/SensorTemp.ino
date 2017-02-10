#include "OneWire.h"

// OneWire DS18S20, DS18B20, DS1822 Temperature Example
// http://www.pjrc.com/teensy/td_libs_OneWire.html
// The DallasTemperature library can do all this work for you!
// http://milesburton.com/Dallas_Temperature_Control_Library

OneWire  sensor_temp(6);  //sensor de temperatura na porta pwn 8

void setup(void) {
  Serial.begin(9600);
}

void loop(void) {
  byte i;
  byte present = 0;
  byte type_s;
  byte data_tmp[12];
  byte addr[8];
  float celsius, fahrenheit;

  if ( !sensor_temp.search(addr)) {
    sensor_temp.reset_search();
    delay(250);
    return;
  }

  sensor_temp.reset();
  sensor_temp.select(addr);
  sensor_temp.write(0x44);

  delay(1000);

  present = sensor_temp.reset();
  sensor_temp.select(addr);    
  sensor_temp.write(0xBE);

 
  for ( i = 0; i < 9; i++) {
    data[i] = sensor_temp.read();
    //Serial.print(data[i], HEX);
    //Serial.print(" ");
  }

  //converte o dado para temperatura
  int16_t raw = (data[1] << 8) | data[0];
  if (type_s) {
    raw = raw << 3;
    if (data[7] == 0x10) {
      raw = (raw & 0xFFF0) + 12 - data[6];
    }
  } else {
    byte cfg = (data[4] & 0x60);
    if (cfg == 0x00) raw = raw & ~7;
    else if (cfg == 0x20) raw = raw & ~3;
    else if (cfg == 0x40) raw = raw & ~1;
  }
  
  celsius = (float)raw / 16.0;
  fahrenheit = celsius * 1.8 + 32.0;
  Serial.print("  Temperature = ");
  Serial.print(celsius);
  Serial.print(" Celsius, ");
  Serial.print(fahrenheit);
  Serial.println(" Fahrenheit");
}

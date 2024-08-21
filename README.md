# ConsoleApp
Задание 1  
Select REF_DEVICE.ID AS REF_DEVICE_ID,PDT AS USAGE_DATE,BRIGADE_CODE From ListCode 
  LEFT JOIN REF_DEVICE ON LISTCODE.DEVICE_SERIAL_NUMBER = REF_DEVICE.SERIAL_NUMBER
WHERE PDT BETWEEN to_date('01-06-2020','dd-mm-yyyy') AND to_date('30-06-2020','dd-mm-yyyy')
  MINUS
 SELECT REF_DEVICE_ID,USAGE_DATE,BRIGADE_CODE FROM DEVICE_USAGE
  WHERE USAGE_DATE BETWEEN to_date('01-06-2020','dd-mm-yyyy') AND to_date('30-06-2020','dd-mm-yyyy')

Задание 2  
json с результатами лежит внутри проекта в папке ConsoleApp

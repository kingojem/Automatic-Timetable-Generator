# Automatic-Timetable-Generator
The University System Often Face Troubles sheduling and assigning periods/Timing to the vast number of Courses ongoing in th Scooling Environment
This Application Help Solve the havoc Faced in Creating the TIme and scheduling the location to enable smooth academic process


#REQUIREMENT
  - Spreadsheet File (xls|xlsx|xlsm|xlsb)
      - Spreedsheet 1:Courses*
          3 Columns***
            - Course Code
                Description: This is a short Code usually provided by the institution to identify/tag a particular subject
                Data Type***: String
                Text Length: (recommended 7 chars, example[cmp 435])
            - Course Status**
                Description: This is the Course Status stating if the Course is Elective(E), Required(R) or Compulsory(C)
                  this usually provided by the institution to ensure priority and population of a given subject
                Data Type***: String
                Text Length: (recommended 1 char, example[E])
            - Course Unit
                Description: This Provided by the institurion is the unit/point of the Course
                Data Type***: Int16
                Text Length: (recommended 1 char example[2])
                
      - Spreedsheet 2:Halls*
          4 Columns***
            - Hall Number
                Data Type***: Int16
                Description: This is a short Code usually provided by the institution to identify/tag a Lecture Hall
                Text Length: (User Defined)
            - Hall Name 
                Description: An Optional Hall Name provided by the Institution
                Data Type***: String
                Text Length: (User Defined)
            - Hall Capacity
                Description: This is the Specified Capacity of the lecture hall
                Data Type***: Enum
                  Big
                  Medium
                  Small
                Text Length: (Enum Values Text Length)
            - Hall Availability
                Description: This is to Notify That the hall is available for use and as tus is set to True
                Data Type: Boolean
                  True
                  False
                Text Length: (Boolean Values Text Length)
                
                
      - System Info
          -Target Framework: .Net Framework 4.7.2
          -Output Type: Windows Application
          -Platform Target: Any CPU(Prefer 32) [Unsafe Code Disallowed]
          - Execution Level: Administrator
          
     #*Third Party Libraries
        -Pdfsharp
           This Converts the Wpf Application into a Pdf Format
           
           #* -The Table name is Saved as The File Name
           #** - This is Provided in the Format Written
           #*** - STRICT The columns and data type and column names is strict
           
           
    #Developers:
      - Emmanuel Ojo
      - Samson Adejero
      - Emmanuel Malieze
      
      This is a GNU AGPLv3 Project
      Copyright: Emmanuel Ojo
      
      


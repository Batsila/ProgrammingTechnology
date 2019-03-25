# Data model

<p>
In this file the data model of accounting system is described.
The objects are in JSON format.
</p>

### Employee

<p>
Object, that contains basic info about worker.
</p>

```json5
{
"id": 42, // number, unique for every employee
"firstName": "Ziggy", // string
"secondName": "Stardust", // string
"role" : "Major", // string
"departmentId": 4, // number
"salaryInfo": {}, // SalaryInfo object
"address": "London, 221B Baker Street", // string
}
```

### SalaryInfo

<p>
Object, that contains info about employee's salary.
I think, that this object also could implement some 
counting salary logic in backend.
</p>

```json5
{
"salaryType": "FIXED", // enum: FIXED or TIME
"rate": null, // number, hours, that worker should work for, if FIXED - null
"salary": 100000000, // (if FIXED - per month, if TIME - per hour),
"bankAccount": "5554GGH63636HDGHDGDG636", // string, field
"paymentType": "P1" // let it be P1 and P2 enum, 
                    // cause I don't give a fuck, 
                    // what "different payment types" means 
                    // in automatic accounting system
}
```

### Department

<p>
Object, that contains basic info about department.
</p>

```json5
{
"id": 4, // number, unique for every department
"name": "Extremely important department",
"employees": [], // array of Employee objects
}
```

### TimeCard

<p>
Object, that contains info date and time, during that employee 
was working that day.
</p>

```json5
{
"employeeID": 42,
"date": "2019-02-30", // string, date in "yyyy-mm-dd" format
"hours": 6.5 // number
}
```
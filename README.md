<!-- PROJECT LOGO -->
<br />
<p align="center">
  <a href="https://github.com/rkelly310/NLogDemo/">
    <img src="images/logging-picture.png" alt="Logo">
  </a>

  <h2 align="center">Writing, Configuring and Locating Log Files using SLF4J and Log4J</h2>

  <p align="center">
    A demo lab instructing users on how to configure structured and unstructured log files, set targets and write logging syntax using Java Runtime, the Simple Logging Facade and the JUnit Framework.
    <br />
    <a href="https://github.com/rkelly310/Log4JDemo"><strong>Explore the docs �</strong></a>
    <br />
    <br />
    <a href="https://github.com/rkelly310/Log4JDemo">View Demo</a>
    �
    <a href="https://github.com/rkelly310/Log4JDemo/issues">Report Bug</a>
    �
    <a href="https://github.com/rkelly310/Log4JDemo/issues">Request Feature</a>
  </p>
</p>



<!-- TABLE OF CONTENTS -->
<details open="open">
  <summary>Table of Contents</summary>
  <ol>
    <li>
      <a href="#about-the-project">About The Project</a>
      <ul>
        <li><a href="#built-with">Built With</a></li>
      </ul>
    </li>
    <li>
      <a href="#getting-started">Getting Started</a>
      <ul>
        <li><a href="#prerequisites">Prerequisites</a></li>
        <li><a href="#installation">Installation</a></li>
        </ul>
        <li><a href="#instructions">Instructions</a></li>
      </ul>
    </li>
<!--
    <li><a href="#roadmap">Roadmap</a></li>
    <li><a href="#contributing">Contributing</a></li>
-->
    <li><a href="#license">License</a></li>
    <li><a href="#contact">Contact</a></li>
<!--
    <li><a href="#acknowledgements">Acknowledgements</a></li>
-->
  </ol>
</details>



<!-- ABOUT THE PROJECT -->
### About The Project

This project is designed for a lab environment to demonstrate proper logging practices in Java environments. Students will configure logging properties and targets, design and test a simple Logger project and organize data by key-value and severity. 

### Built With

### Built With

* [Eclipse IDE] Version: 2021-03 (4.19.0) (https://www.eclipse.org/downloads/)
* [Java Development Kit v.11.28](https://openjdk.java.net/projects/jdk/11/)
* Chromedriver.exe (or another browser equivalent) existing in your System PATH) https://chromedriver.chromium.org/downloads
<br>

**Please ensure you also download the following plugins if you do not have them already:**  

* [Junit 5] Testing Framework
* SLF4J version 1.7.25

<!-- GETTING STARTED -->
## Getting Started

### Prerequisites

None, other than an installation of Eclipse or another Java IDE and Maven. Students can download the full solution, or follow the steps below to develop the Logger application on their own.

### Installation

Simply clone the repo to see the full solution:
   ```sh
   git clone https://github.com/rkelly310/Log4JDemo.git
   ```
<!-- Instructions -->
## Instructions
### Task 1: Set Up your pom.xml file and create a basic logging app

The first step in this lab is to create a simple Maven application. Once the **pom.xml** is initialized, add the following dependencies to your POM file.  

```java
<!-- https://mvnrepository.com/artifact/org.slf4j/slf4j-api -->
	<dependency>
    	<groupId>org.slf4j</groupId>
    	<artifactId>slf4j-api</artifactId>
    	<version>1.7.25</version>
	</dependency>
	<!-- https://mvnrepository.com/artifact/org.slf4j/slf4j-simple -->
<dependency>
    <groupId>org.slf4j</groupId>
    <artifactId>slf4j-simple</artifactId>
    <version>1.7.30</version>
</dependency>
```
Then, create a java file within your **src** package folder, and name it **LoggerDemo.java**. Paste in the following code:  

```java
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
public class MyLogger {
   public static void main(String[] args) {
      //Creating our custom Logger
      Logger logger = LoggerFactory.getLogger("FirstLogger");

      //Some simple outputs
      logger.info("Simply an update");
      logger.error("Example of an error");
   }
}
```
If we run the file, we should get get the following:  
```java
[main] INFO FirstLogger - Simply an update
[main] ERROR FirstLogger - Example of an error
```

### Task 2: Implement Log4J to configure log structure
Now that we have initialized the Simple Logging Facade, we can add Log4J to change the structure of our log files. We need to download the binding for using log4j and slf4j together. Remove the **slf4j-simple** dependency from your POM and add the following snippet:  

```java
<dependency>
    <groupId>org.slf4j</groupId>
    <artifactId>slf4j-log4j12</artifactId>
    <version>1.7.30</version>
    <scope>test</scope>
</dependency>
<dependency>
    <groupId>log4j</groupId>
    <artifactId>log4j</artifactId>
    <version>1.2.17</version>
</dependency>
```
Log4j will help us achieve best practices is the use of its configuration file. With log4j, there is no need to manually modify the vast amounts of logging statements that inevitably pile up within any  application. Within the **src/main/resources** directory, create the a filed called log4j.properties and add the starting options here: 

```java
log4j.rootLogger=DEBUG, A1
log4j.appender.A1=org.apache.log4j.ConsoleAppender
log4j.appender.A1.layout=org.apache.log4j.PatternLayout
log4j.appender.A1.layout.ConversionPattern=%d [%t] %-5p %c - %m%n
```
These are all the default starting options from the log4j documentation found [here](https://logging.apache.org/log4j/2.x/). Before running the program again, we need to make sure Eclipse knows to include this file within the classpath. C**lick Run -> Run Configurations… -> Dependencies -> Classpath Entries -> Click Advanced on the right -> Add Folders**.   
### Task 3. Implement Best Logging Practices in our Application  
Lastly, lets add some code for an input-based application that allows a user to make an appointment with the bank, choosing from a list of appointment types and available days to schedule. This avoids bringing in unnecessary dependencies like a REST API, but will also allow us to demonstrate our logger. Replace **MyLogger.java** with the following:  

```java
import java.util.Scanner;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.slf4j.MDC;

public class LoggerDemo {
    public static void main(String[] args) {
    	String sessionId = "123456";
    	MDC.put("sessionId", sessionId);
        Logger logger = LoggerFactory.getLogger("FirstLogger");
        System.out.print("Welcome to Wells Fargo Appointments. What type of account assistance would you like?\n");
        Scanner in = new Scanner(System.in);
        String dentistName = in.next();
        String[] dentist = { "Personal", "Business", "Other" };
        String[] days = { "Monday", "Wednesday", "Thursday", "Saturday" };
        boolean dentistFound = false;
        boolean dayFound = false;

        for (String s : dentist) {
            if (s.equals(dentistName)) {
                System.out.print("One Moment Please...\n");
                logger.info("You selected: " + dentistName);
                dentistFound = true;
                break;
            }
        }

        if (dentistFound) {
            System.out.println("When would you like to book your appointment?");
            String dayPicked = in.next();
            for (String p : days) {
                if (p.equals(dayPicked)) {
                    System.out.println("You are booked for " + dayPicked);
                    logger.info("Appointment booked on " + dayPicked);
                    dayFound = true;
                    System.exit(0);
                }
            }
            if (dayFound == false) {
                logger.error("Sorry, we only available for in-person appointments on Monday, Wednesday, Thursday and Saturday.");
                logger.info("Exiting application.");

                System.exit(0);
            }
        }
        else {
            logger.error("Invalid assistance option. Please enter 'Personal', 'Business' or 'Other'");
        }
        logger.info("Exiting application.");
        System.exit(0);
    }
}
```
As it stands, MyLogger.java contains a simple flow of user input asking the following:
<br>
1. The type of appointment they would like to make.
<br>
2. The day that they would like to book their appointment, from the available days in the days array.  
<br>

Let’s run the program with the same inputs: Business, Sunday.
We should see the following:  
```
Welcome to Wells Fargo Appointments. What type of account assistance would you like?
Business
One Moment Please...
2021-04-22 18:48:52,552 [main] INFO  FirstLogger - You selected: Business
When would you like to book your appointment?
Sunday
2021-04-22 18:48:57,772 [main] ERROR FirstLogger - Sorry, we only available for in-person appointments on Monday, Wednesday, Thursday and Saturday.
2021-04-22 18:48:57,772 [main] INFO  FirstLogger - Exiting application.
```   
### Task 4: Making Queryable Logs  
To transform our logged outputs into JSON objects, we could typically use Logback. There is extensive documentation on how to achieve this with logback and can be found [here](http://logback.qos.ch/documentation.html). In our application, we can set our log4j.properties file to contain a custom JSON formatter. Modify the file to contain the following:  
```java
log4j.rootLogger=INFO, file
log4j.appender.file=org.apache.log4j.RollingFileAppender
log4j.appender.file.File=log/logging.log
log4j.appender.file.MaxFileSize=10MB
log4j.appender.file.MaxBackupIndex=10
log4j.appender.file.layout=org.apache.log4j.PatternLayout
log4j.appender.file.encoding=UTF-8
log4j.appender.file.layout.ConversionPattern={"timestamp":"%d","logUpdate": "%5p [%t] (%F:%L)","status":"%m","sessionID": "%X{sessionId}}%n"
```  
Now we have a logging.log file in our solution directory containing the following in JSON format:  
```text
{"timestamp":"2021-04-22 11:55:18,011","logUpdate": " INFO [main] (LoggerDemo.java:24)","status":"You selected: Business","sessionID": "123456}
"{"timestamp":"2021-04-22 11:55:22,878","logUpdate": " INFO [main] (LoggerDemo.java:36)","status":"Appointment booked on Saturday","sessionID": "123456}
"{"timestamp":"2021-04-22 18:45:59,677","logUpdate": " INFO [main] (LoggerDemo.java:24)","status":"You selected: Business","sessionID": "123456}
"{"timestamp":"2021-04-22 18:52:07,963","logUpdate": " INFO [main] (LoggerDemo.java:24)","status":"You selected: Business","sessionID": "123456}
"{"timestamp":"2021-04-22 18:52:10,529","logUpdate": "ERROR [main] (LoggerDemo.java:42)","status":"Sorry, we only available for in-person appointments on Monday, Wednesday, Thursday and Saturday.","sessionID": "123456}
"{"timestamp":"2021-04-22 18:52:10,529","logUpdate": " INFO [main] (LoggerDemo.java:43)","status":"Exiting application.","sessionID": "123456}
"
```text  

<!-- LICENSE -->
## License

Distributed under the MIT License. See `LICENSE` for more information.

<!-- CONTACT -->
## Contact

Project Link: [https://github.com/rkelly310/Log4JDemo](https://github.com/rkelly310/Log4JDemo)



<!-- MARKDOWN LINKS & IMAGES -->
<!-- https://www.markdownguide.org/basic-syntax/#reference-style-links -->
[contributors-shield]: https://img.shields.io/github/contributors/othneildrew/Best-README-Template.svg?style=for-the-badge
[contributors-url]: https://github.com/othneildrew/Best-README-Template/graphs/contributors
[forks-shield]: https://img.shields.io/github/forks/othneildrew/Best-README-Template.svg?style=for-the-badge
[forks-url]: https://github.com/othneildrew/Best-README-Template/network/members
[stars-shield]: https://img.shields.io/github/stars/othneildrew/Best-README-Template.svg?style=for-the-badge
[stars-url]: https://github.com/othneildrew/Best-README-Template/stargazers
[issues-shield]: https://img.shields.io/github/issues/othneildrew/Best-README-Template.svg?style=for-the-badge
[issues-url]: https://github.com/othneildrew/Best-README-Template/issues
[license-shield]: https://img.shields.io/github/license/othneildrew/Best-README-Template.svg?style=for-the-badge
[license-url]: https://github.com/othneildrew/Best-README-Template/blob/master/LICENSE.txt
[linkedin-shield]: https://img.shields.io/badge/-LinkedIn-black.svg?style=for-the-badge&logo=linkedin&colorB=555
[linkedin-url]: https://linkedin.com/in/othneildrew
[product-screenshot]: images/screenshot.png

# Technical Assessment
# Veritone - CRMBilling 

## Overview

This repository contains the technical assessment for 
CRM Billing Dashboard for a Subscription-Based Business

## Getting Started



### Prerequisites

- Git
- Programming language as specified in the assessment (C#/Angular)
- Any additional frameworks or libraries mentioned in the assessment instructions
- Visual studio code + Visual studio for running the code, Sql server

### Setup Instructions

In the github repo there are some projects and files:
1. CRMBillingDB.bak file:
	This is the backup of the Restful api,
	Download the file and Restore it on sql server management

2. CRMBilling folder:
   This folder contains the code for Task 1: Restful API with .Net 6.0+
   This folder needs to be download and after compiling the code and running the solution,
   the api will be up at the domain: https://localhost:44322

3. crm-billing-app folder:
   This folder contains the code for Task 2: Angular UI for Graphical Data Representation
   This folder needs to be download and after that run the code with vs code - execute with 'npm start'
   The UI will be up at the domain: http://localhost:4200

4. BillingDSChallenge folder:
   This folder contains the code for Task 3: Algorithm and Data Structures Challenge (option A).
   This folder needs to be download and after that run all the test with tab 'Test' --> 'Run All Tests' on Visua Studio
   
5. Veritone.postman_collection.json file:
   This file is a collection of Postman collection for Calling CRMBilling api 
   After Download the file import it on Postman

## Assessment Structure

1. CRMBilling - api build with dot.net core 8 C#/Angular
   Two projects:
   1. CRMBilling - Asp.Net Rest api project including BillingsController.
   2. CRMBilling.Core - Business project
2. crm-billing-app - Angular UI
   1. dashboard.component - The component that include the Dashboard data with table data + Graphical Representation
   2. billing-service - service that called to rest api with rxjs Observable async calls.
   3. model - 
      billing-record - Billing record interface model
	  filter - Filter the get api model
	  response - ResponseData from server object model
3. BillingDSChallenge - Algorithm for getting top K customers by total billing amount, build with dot.net core 8
   Two projects:
   1. BillingDSChallenge - Algorithm + models
   2. BillingDSChallengeTests - Testing with Microsoft UnitTesting
## Requirements

- See attached link - 
  https://docs.google.com/document/d/1t-sMStrWh7EmeAQ3H46DmJCzlGNoy_ZjqvuVl1KjECI/edit?usp=sharing

## Implementation

1. CRMBilling - 
   - Creating 1 project for Rest api and one for business core of the app
   - Program.cs - define all Dependency Injection objects, sqlserver db context, use serilog for logging, define distributed sql server cache, define cors.
   - appsettings.json - solution configuration file
   - BillingController - define RestFul api 'api/Billings', using ICRMBillingService with Dependency Injection.
 2. CRMBilling.Core - 
   - CRMBillingDBContext - define db context EntityFramework db first
   - Model - BillingRecord - main object model of Billing, CreateBillingRecordDto - create billing record dto - for insert new BillingRecord,
   adding validation into model with 'DataAnnotations', defualt and custom
   - Repository - Using repository design pattern for calling db
		- Geting all data with filtering and distributed cache
		- Save new billing record
   - Services - using ICRMBillingService interface and Implementation for calling Restful operations, 
   using custom logging and error handler.
   - Using mapper between BillingRecord to dto
   - Validation custom attributes
   - Using async await for asynchronous programming
   - Seperate tiers - Restful api and Business tiers
   - Using immutable data properties on BillingRecord
 3. BillingDSChallenge - 
    Implement Top K solution:
	Aggregation:
			- Iterate all BillingRecord objects to calculate total amount for each customer usinf HashMap
			- Time: O(n) Space: O(m) where m is number of unique cusomers
        Selection: 
			- Use min heap with priority queue for getting top K customers
			  Time: O(m log k) for all customers
	Result: Extract the results from heap in reverse order
		
	This solution efficient for large datasets because:
	- We need to scan billing records once
 	- The heap maintains always just k elments and not all data
4. crm-billing-app - 
   - Implement UI with angular 
	 - Service-based approach for data fetching
	 - Reactive programming with RxJS
	 - Components 
	 - Data visualization
		- line chart for billing records over Time	
		- doughnut chart for subscription distribution
		- interactive filtering
		- table design for all billing records
	 - Responsive design 
	 - Loading and erorrs
	 - filtering

# Simple Automation 

## Assumptions and Considerations
	* Timeout for each request considered as 5 seconds, enforced by failing the tests.
	* Tests for IE are passing on Windows 7 and failing on Windows 10.
	* I have followed instructions mentioned on this page: https://code.google.com/p/selenium/wiki/InternetExplorerDriver#Required_Configuration
	* Ideally I would like to use a proxy component to check the HTTP status code for respective steps to assert, considering this as sample solution I am leaving that out for later.
	* I have created ContactUsTestTemplate to implement the various workflow steps on this pages. 
	* I have created 3 classes to test browser specific test, these classes can include any browser specific code which might be required for implementing the test correctly.	
	
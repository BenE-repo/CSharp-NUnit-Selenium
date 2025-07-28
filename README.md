# CSharp-NUnit-Selenium

Example of previous work using C# / Selenium / NUnit. 

The website under test was (very basically) a CRM-type system for training providers. Applicants would apply for training,
become learners, and take courses. This training was done on behalf of organisations, which had officers etc. 

- All coded by me, bar the occasional copy 'n' paste from StackOverflow
- Had to remove some sensitive and important things, so would be very, very surprised if it even compiles now
- Things of interest:
    - /POMs: Contains Page Object Models for the site under test
        - /Components contains classes for each of the types of components used on the site, dropdown boxes, tables etc. 
            The idea was ultimately to have all actions, (and in future some assertions) related to the component here as possible in an attempt to keep the actual code for the tests as clean as possible. 'SingleSelect' is one of the more interesting ones; Just a dropdown list where a single item can be selected, but the custom component the devs used made the usual way of interacting with single selects via Selenium impossible. I needed to actually click the component and then select the item I wanted. Having two actions in the tests everytime would be ugly, hence a class to obfuscate/prettify the process a bit.
        - The other subfolders contain the POMs broken up broadly by the area of the site. Except for the ones I hadn't 
            finished organising yet... 
    - /Tests: Contains NUnit tests
    - /Utils: I directly accessed the DB at times, mostly for the setup and teardown of test data, but also for some
        direct data checking after the creation/editing of records.

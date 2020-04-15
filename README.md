# CodeClinic_C-Sharp
 
 > This repo is for learning and trying things out. The project ideas originate from the LinkedIn Learning Course: "Code Clinic: C#"

 # Project 1: Barometer
 ## Tasks
 * Input of barometric data from datasoure
    * Example textfile for first implementation
    * Later data from an API or something
* Overview
    * Create a function that gets a start date, end date and the data passed in and returns cartesian coordinates
    * Create a function that calculates the slope of barometric pressure of the given timerange 
    * Plot the graph with a technologie of your choice
        * UWP
        * Xamarin.Forms
        * ASP.&#8203;net

# Project 2: Where Am I?
## Task
* Find the geolocation of the machine, currently running your code
* Overview
    * Show
        * Latitude
        * Longitude
        * Location on Map
        * Accuracy as circle (e.g.)
    * Use a technologie of your choice
        * UWP
        * Xamarin.Forms
        * ASP.&#8203;net
            * Show location of client not server

# Project 3: Eight Queens
## Task
* Find an algorithm for the eight queens problem
    * Place eight queens on a chessboard so that no queen can attack each other
    * Show all 92 solutions
* Overview
    * General approach
        * Modular not monolithic
    * Design
        * 1D representation of a 8x8 board
            * Array of eight fields (columns)
            * Each field holds a integer number (number of row)
        * Collision detection
            * Column
                * Each field holds just one number
                * No possibility to have more than one queen in a column
            * Row
                * Each number in a field has to be unique over all fields
            * Diagonal collision
                * Check the vertical and horizontal differences between two queens
                * Collision is detected if they are equal

# Project 4: Accessing peripherals
## Task
* Convert mouse movement into music pitches
    * Instrument is silent until mouse button is held down
    * Up and down movement changes the pitch
    * Left and right movement changes the volumn
    * Music stops when mouse button lifts again
* Overview
    * Surface-Technology
        * UWP-App
            * Observe mouse clicks
            * Observe mouse movement
    * Generating Sound
        * Use of nuget package NAudio v1.10
            * SignalGenerator class as sine wave generator
            


 

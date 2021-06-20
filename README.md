<!-- PROJECT LOGO -->
<br />
<p align="center">
  <a href="https://github.com/rkelly310/NLogDemo/">
    <img src="images/logging-picture.png" alt="Logo">
  </a>

  <h2 align="center">Writing Characterization Tests for Inherited Legacy Code</h2>

  <p align="center">
    A demo lab instructing users on how to write characterization tests given an inherited codebase, as well as how to properly refactor the code to be most efficient and maintain modern programming standards. 
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

This project is designed for a lab environment to demonstrate proper characterization test creation in .NET environments. Students will dissect the preexisting code of a Discount Calculator application, develop tests to best understand the given codebase, and make edits to modernize the codebase. Students will use the MSTest framework in Visual Studio or VS Code.
### Built With

### Built With

* [Visual Studio] 
<br>

**Please ensure you also download the following plugins if you do not have them already:**  

* [MSTest Framework]

<br>
<!-- GETTING STARTED -->
## Getting Started

### Prerequisites

None, other than an installation of Visual Studio. Students can download the full solution with example written tests, or follow the steps below to develop the their own tests.

### Installation

Simply clone the repo to see the full solution:
   ```sh
   git clone https://github.com/rkelly310/Log4JDemo.git
   ```
<!-- Instructions -->
## Instructions
### Task 1: Introducing Tests

We have some legacy code. We need to make changes. To make changes we need to introduce tests first. We might have to change some code to enable testing. We need to introduce so-called Seams (see Michael Feathers' Working Effectively with Legacy Code). Changing code without test is risky, so we want to:  
<br>
-Only change as little code as possible.  
-Rely on automated Refactoring tools as much as possible.  
-You must not change the public API of the class.  

The given code calculates the discount for a purchase in our online shop. The main logic is in Discount. We also know that we **cannot modify MarketingCampaign class** because it is used by other teams as well.  
Take a look at the **Discount.cs** code below. How can this be tested?

```csharp
namespace CharacterizationCode
{
    public class Discount
    {
        private readonly MarketingCampaign marketingCampaign;

        public Discount()
        {
            this.marketingCampaign = new MarketingCampaign();
        }

        public Money DiscountFor(Money netPrice)
        {
            if (marketingCampaign.IsCrazySalesDay())
            {
                return netPrice.ReduceBy(15);
            }
            if (netPrice.MoreThan(Money.OneThousand))
            {
                return netPrice.ReduceBy(10);
            }
            if (netPrice.MoreThan(Money.OneHundred) && marketingCampaign.IsActive())
            {
                return netPrice.ReduceBy(5);
            }
            return netPrice;
        }
    }
}
```
### Task 2: Modifying Code

At this point we have an idea of how the code works, and are aware that Discount is closely dependent on the MarketingCampaign class. How can we decouple this dependency? Use dependency injection to invert the control and create an interface between the two classes:

```csharp
    public class MarketingCampaign
    {
        public bool IsActive()
        {
            return (long) DateTime.Now.TimeOfDay.TotalMilliseconds % 2 == 0;
        }

        public bool IsCrazySalesDay()
        {
            return DateTime.Now.DayOfWeek.Equals(DayOfWeek.Friday);
        }
    }
```  

### Task 3: Make Tests Pass  
Now that we have improved the codebase with decoupling and dependency injection, lets continue developing tests to demonstrate the behavior of the codebase (hint: mocking and stubbing can be used here). View the Solution with some example tests in the 'Solution' section of the repository.  


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

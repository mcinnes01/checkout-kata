# checkout-kata

**Problem Synopsis**
A supermarket requires a working checkout. MVP is to scan products and periodically ask for a total price, considering any special offers that apply to the product.

Items:
| SKU      | Unit Price |
| :---     |   ---:     |
| A99      | 0.50       |
| B15      | 0.30       |
| C40      | 0.60       |

Special Offers:
| SKU    | Quantity | Offer Price |
| :---   | :----:   |        ---: |
| A99    | 3        | 1.30        |
| B15    | 2        | 0.45        |

The checkout accepts items scanned in any order, so that if we scan a pack of Biscuits (B15), an apple (A99) and another pack of biscuits, we’ll recognise two packs of biscuits and apply the discount of 2 for 45p. Prove your solution works for this scenario.

**Format**
Manage your time effectively to progress through the main points, do not get distracted on earlier deliverables (such as with very detailed testing). For the purposes of this short kata, showing the primary deliverables and talking about broader work is fine.

**Deliverables**
Please push your work to a remote git repository (e.g. GitHub) and share the location and any required credentials with us. Commit as you go to show your working process, rather than just one big commit at the end. If you can’t share a public repository, please zip the local git repository and send that so we can still see your commits.
Work your way through this checklist:

* Create a new solution
* Include a class library for the logic
* Include a test library for unit tests (feel free to use whatever test library you are most comfortable with)
* Prove you can scan an item at a checkout
* Prove you can request the total price
* Introduce special offers
* Amend your prior implementation to consider offers on items
* Prove you can request the total price inclusive of offers
* This kata covers just the middleware, do not be concerned with UI or data access.

**Kata Review**
Your solution will be measured against the deliverables above, and how you managed your time across them. No one solution is “correct”, but we are interested in:
* Your process
* Testable code
* Maintainable code
* Abstraction at sensible points
* How you would refactor your solution to deliver future requirements

I have decided to use a decorator pattern, the reason for this is that each product can then be treated by multiple pricing methods, and will only be affected by each decorator they pass through, if they meet the requirements of that decorator. This means a product could be part of a multi-buy for example, but also receive a percentage discount i.e. for membership card holders.

This is a fairly expandable solution, however it doesn't cover scenarios such as mix and match like a meal-deal or total basket discounts. To achieve this you could have decorators for different levels, so I have chosen a product level decorator, but you could make a "Bag" level decorator to apply discounts across products or to the receipt as a whole.

I'm not using any DI for this basic example, but you could register the decorators chaining in your DI container, meaning you would only need to inject an IReciptItem in to your checkout, essentially allowing you to decouple the specific discount implementations from the checkout service.

This however still comes with a drawback that you cannot change the order in which the decorators are applied. To further increase flexibility you could implement something that allows you to configure the order in which to run the items through the decorators. This would probably make sense to have some form of data store, as you may want to run different configurations for different people. For example geographic location, special offers between dates, user demographics (pensioner discounts, family cards), etc.

To do this a factory patterns could be used to return the IReceiptItem decorators chain specifically for the customer.

Other considerations are strategy pattern, which could identify if there are any discounts applicapable to a product and apply the discount. Again coupling with a factory would help resolve the discounts applicable without having to loop over each discount.
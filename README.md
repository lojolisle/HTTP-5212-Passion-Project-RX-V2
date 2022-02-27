# HTTP-5212-Passion-Project-RX-V2

Few highlights of my MVP project built using MVC design pattern :

1) Uses Models  and database tables created using Entity Relationship Framework

2) Use of View Models to pass data from multiple entities to the views

3) Implemented CRUD for all three Models; Prescription, Drug, PrescriptionDrug

4) PrescriptionDrug Model works as a bridge for Drug and Prescription Entities. It uses a composite key (PrescriptionId and DrugID) as its primary key

5) Seperation of Controllers into more specific DataControllers that talks directly to the Models and on top, another layer of Controllers to interact with 
   DataController layer

6) Use of Views for user level interactions



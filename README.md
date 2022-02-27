# HTTP-5212-Passion-Project-RX-V2

My Passion Project is an humble attempt of automating the processes from generation of a Doctor Prescription to the dispensing of drugs at a Pharmacy.
The idea is to use a shared database between Doctor's Office and Pharamcy allowing the Pharmacy to receive a prescription as soon as it is created or prescribed by a doctor. This would benefit a Pharmacy in avoiding manual entry of a Prescription into the System and can instead utilise this time in dispending the medicine  and can also help avoid human errors.

Few highlights of my MVP built using MVC design pattern :

1) Uses Models  and database tables created using Entity Relationship Framework

2) Use of View Models to pass data from multiple entities to the views

3) Implemented CRUD for all three Models; Prescription, Drug, PrescriptionDrug

4) PrescriptionDrug Model works as a bridge for Drug and Prescription Entities. It uses a composite key (PrescriptionId and DrugID) as its primary key

5) Seperation of Controllers into more specific DataControllers that talks directly to the Models and on top, another layer of Controllers to interact with 
   DataController layer

6) Use of Views for user level interactions

Disclaimer: This Project is for demostration purposes only



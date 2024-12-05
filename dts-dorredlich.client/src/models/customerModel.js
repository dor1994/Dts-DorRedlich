export class CustomerModel {
 
    constructor(customerId, customerName, requestedTime, createdAt = null, id = 0) {
      this.id = id;
      this.customerId = customerId;
      this.customerName = customerName;
      this.requestedTime = requestedTime;
      this.createdAt = createdAt; 
    }

}
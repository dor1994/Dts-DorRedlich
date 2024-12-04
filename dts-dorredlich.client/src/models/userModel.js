export class UserModel {
 
    constructor(username, password, firstName, id = 0) {
      this.id = id;
      this.username = username;
      this.password = password;
      this.firstName = firstName;
    }
  }
export enum Role {
  Dev = 'Desenvolvedor',
  Admin = 'Administrator'
}

export class User {
  id: number;
  username: string; 
  email: string; 
  role: Role;
  token?: string;

  init(_data?: any) { 
    if (_data) {
      this.id = _data["id"];
      this.username = _data["userName"];
      this.email = _data["email"]; 
      this.role = _data["role"];
      this.token = _data["token"];
    }
  }

  static fromJS(data: any): User {
    data = typeof data === 'object' ? data : {};
    let result = new User();
    result.init(data);
    return result;
  }
}

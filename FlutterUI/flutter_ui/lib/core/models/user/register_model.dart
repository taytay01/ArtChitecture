import 'dart:convert';
import '../entity.dart';

@entity
class RegisterModel {
  String firstName;
  String lastName;
  String email;
  String password;

  RegisterModel(
    this.firstName,
    this.lastName,
    this.email,
    this.password,
  );

  String toJson() {
    return json.encode({
      "firstName": firstName,
      "lastName": lastName,
      "email": email,
      "password": password,
    });
  }
}

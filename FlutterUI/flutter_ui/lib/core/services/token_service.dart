import 'package:flutter_ui/core/models/user/user_model.dart';
import 'package:flutter_ui/core/utilities/dependency_resolver.dart';
import 'package:flutter_ui/core/utilities/service.dart';
import 'package:jwt_decode/jwt_decode.dart';

class TokenService extends Service {
  decodeToken(String? token) {
    if (token != null && token != "") {
      return Jwt.parseJwt(token);
    }
    return null;
  }

  Future<String?> getToken() async {
    return await sessionService.get("token");
  }

  Future<void> setToken(String? token) async {
    await sessionService.set('token', token);
  }

  Future<void> removeToken() async {
    await sessionService.remove('token');
  }

  Future<String?> getRefreshToken() async {
    return await sessionService.get("refresh-token");
  }

  Future<void> setRefreshToken(String? token) async {
    await sessionService.set('refresh-token', token);
  }

  Future<void> removeRefreshToken() async {
    await sessionService.remove('refresh-token');
  }

  Future<String?> getClientId() async {
    return await sessionService.get("client-id");
  }

  Future<void> setClientId(String? clientId) async {
    await sessionService.set('client-id', clientId);
  }

  Future<void> removeClientId() async {
    await sessionService.remove('client-id');
  }

  Future<bool> isTokenExpired() async {
    String? token = await getToken();

    bool? isExpired;
    if (token != null && token != "") isExpired = Jwt.isExpired(token);

    return isExpired != null ? isExpired : true;
  }

  Future<DateTime?> getTokenExpirationDate() async {
    var token = await getToken();

    print(token);
    if (token != null && token != "") {
      return Jwt.getExpiryDate(token);
    }

    return null;
  }

  Future<List<String>> getUserRolesWithJWT() async {
    var token = decodeToken(await getToken());

    print(token);
    List<String> _roles = [];

    if (token != null) {
      Map t = token;

      t.keys.forEach((element) {
        if (element.endsWith("/role")) {
          if (t[element] is String) {
            _roles.add(t[element]);
          } else if (t[element] is List) {
            for (int i = 0; i < t[element].length; i++)
              _roles.add(t[element][i]);
          }
        }
      });

      return _roles;
    }

    return _roles;
  }

  Future<UserModel?> getUserWithJWT() async {
    var token = decodeToken(await getToken());

    print(token);
    if (token != null) {
      UserModel userModel = UserModel(0, "", "", "", true);
      Map t = token;

      t.forEach((key, dynamic value) {
        String k = key;

        if (k.endsWith("/nameidentifier")) {
          userModel.id = int.parse(value.toString());
        } else if (k.endsWith("/name")) {
          userModel.firstName = value.toString();
        } else if (k.endsWith("/surname")) {
          userModel.lastName = value.toString();
        } else if (k.endsWith("email")) {
          userModel.email = value.toString();
        } else if (k.endsWith("status")) {
          userModel.status =
              value.toString().toLowerCase() == "true" ? true : false;
        }
      });

      return userModel;
    }

    return null;
  }
}

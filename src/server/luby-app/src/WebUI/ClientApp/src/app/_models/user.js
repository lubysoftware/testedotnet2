"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.User = exports.Role = void 0;
var Role;
(function (Role) {
    Role["Dev"] = "Desenvolvedor";
    Role["Admin"] = "Administrator";
})(Role = exports.Role || (exports.Role = {}));
var User = /** @class */ (function () {
    function User() {
    }
    User.prototype.init = function (_data) {
        debugger;
        if (_data) {
            this.id = _data["id"];
            this.username = _data["userName"];
            this.email = _data["email"];
            this.role = _data["role"];
            this.token = _data["token"];
        }
    };
    User.fromJS = function (data) {
        data = typeof data === 'object' ? data : {};
        var result = new User();
        result.init(data);
        return result;
    };
    return User;
}());
exports.User = User;
//# sourceMappingURL=user.js.map
import { AbstractControl, ValidationErrors, ValidatorFn } from '@angular/forms';

export function passwordValidator(): ValidatorFn {
  return (control: AbstractControl): ValidationErrors | null => {
    const value: string = control.value || '';

    const hasUpperCase = /[A-Z]/.test(value);
    const hasLowerCase = /[a-z]/.test(value);
    const hasNumber = /[0-9]/.test(value);
    const hasSpecialChar = /[\W_]/.test(value); // includes special characters like !@#$%, etc.

    const valid = hasUpperCase && hasLowerCase && hasNumber && hasSpecialChar;

    return valid ? null : { strongPassword: true };
  };
}

export function matchPasswords(passwordKey: string, confirmPasswordKey: string): ValidatorFn {
  return (group: AbstractControl): ValidationErrors | null => {
    const password = (group.get(passwordKey)?.value || '').trim();
    const confirmPassword = (group.get(confirmPasswordKey)?.value || '').trim();

    if (password !== confirmPassword) {
      group.get(confirmPasswordKey)?.setErrors({ passwordMismatch: true });
      return { passwordMismatch: true };
    }

    return null;
  };
}

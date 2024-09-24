import { useState } from "react";
import { MIN_PASSWORD_LENGTH, MIN_USERNAME_LENGTH } from "../helpers/constants";

interface Values {
  [key: string]: string;
}
type FormType = "login" | "register";

export default function useForm(formType: FormType) {
  const initialValues: Values =
    formType === "login"
      ? { email: "", password: "" }
      : { email: "", password: "", username: "", passwordConfirmation: "" };

  const [values, setValues] = useState<Values>(initialValues);
  const [errors, setErrors] = useState<Values>({});
  const [showErrors, setShowErrors] = useState(true);

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setShowErrors(false);
    const { name, value } = e.target;
    setValues({ ...values, [name]: value });
    setErrors((prevErrors) => ({ ...prevErrors, [name]: "" }));
    console.log(errors);
  };

  const handleSubmit = (e: React.FormEvent<HTMLFormElement>): boolean => {
    e.preventDefault();
    setShowErrors(true);
    const validationErrors = validateForm();
    if (Object.values(validationErrors).some((err) => err)) {
      setErrors(validationErrors);
      return false;
    }
    console.log("Form submitted", values);
    return true;
  };

  const validateForm = (): Values => {
    const newErrors: Values = {};

    if (!values.email.trim()) {
      newErrors.email = "Email is required";
    } else if (
      !values.email.match(
        /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|.(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/
      )
    ) {
      newErrors.email = "Email is invalid";
    }

    if (!values.password.trim()) {
      newErrors.password = "Password is required";
    } else if (values.password.length < MIN_PASSWORD_LENGTH) {
      newErrors.password = `Password must be at least ${MIN_PASSWORD_LENGTH} characters`;
    }

    if (formType === "register") {
      if (!values.username.trim()) {
        newErrors.username = "Username is required";
      } else if (values.username.length < MIN_USERNAME_LENGTH) {
        newErrors.username = `Username must be at least ${MIN_USERNAME_LENGTH} characters`;
      }

      if (!values.passwordConfirmation.trim()) {
        newErrors.passwordConfirmation = "Password confirmation is required";
      } else if (values.password !== values.passwordConfirmation) {
        newErrors.passwordConfirmation = "Passwords do not match";
      }
    }

    return newErrors;
  };

  return {
    handleChange,
    handleSubmit,
    values,
    errors,
    showErrors,
  };
}

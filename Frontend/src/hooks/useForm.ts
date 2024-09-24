import { useState } from "react";
import { MIN_PASSWORD_LENGTH } from "../helpers/constants";

interface Values {
  [key: string]: string;
}

export default function useForm() {
  const [values, setValues] = useState<Values>({});
  const [errors, setErrors] = useState<Values>({});

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const { name, value } = e.target;
    setValues({ ...values, [name]: value });
    setErrors((prevErrors) => ({ ...prevErrors, [name]: "" }));
  };

  const handleSubmit = (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();
    const validationErrors = validateForm();
    if (Object.values(validationErrors).some((err) => err)) {
      console.log("Validation errors", validationErrors);
      setErrors(validationErrors);
      return;
    }
    console.log("Form submitted", values);
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

    return newErrors;
  };

  return {
    handleChange,
    handleSubmit,
    values,
    errors,
  };
}

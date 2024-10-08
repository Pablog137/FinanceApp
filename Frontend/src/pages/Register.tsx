import { useState } from "react";
import Spinner from "../components/UI/Spinner";
import { useDarkMode } from "../context/DarkModeContext";
import Error from "../components/UI/Error";
import useForm from "../hooks/useForm";
import { setCookie, setItemLocalStorage } from "../helpers/localStorage";
import { useNavigate } from "react-router";

export default function Register() {
  const { textColor, inputStyles } = useDarkMode();
  const {
    errors,
    handleChange,
    handleSubmit,
    showErrors,
    values,
    serverError,
    setServerError,
  } = useForm("register");

  const [isLoading, setIsLoading] = useState(false);
  const navigate = useNavigate();

  const handleRegister = async (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();
    const formSubmitted = handleSubmit(e);
    if (!formSubmitted) return;

    setIsLoading(true);

    try {
      const response = await fetch(
        `${import.meta.env.VITE_URL}/api/auth/register`,
        {
          method: "POST",
          headers: {
            "Content-Type": "application/json",
          },
          body: JSON.stringify(values),
        }
      );

      const data = await response.json();
      if (response.ok) {
        setItemLocalStorage("token", data.token);
        setItemLocalStorage(
          "user",
          JSON.stringify({ email: data.email, username: data.username })
        );
        setCookie("refresh_token", data.refreshToken);
        // Redirect to the dashboard
        navigate("/dashboard");
      }
    } catch (err: any) {
      setServerError(err.message);
    } finally {
      setIsLoading(false);
    }
  };

  const currentError =
    errors?.username ||
    errors?.email ||
    errors?.password ||
    errors?.passwordConfirmation ||
    serverError;

  return (
    <>
      <div className="bubbles">
        <div className="bubble bubble3"></div>
        <div className="bubble bubble4"></div>
        <div className="bubble bubble5"></div>
      </div>

      <div className="flex justify-center items-center min-h-screen">
        <div
          className={`${textColor} grid place-items-center px-6 login-container p-16`}
        >
          <h1 className="text-3xl md:text-4xl text-center font-bold py-8 md:p-10">
            Create an account
          </h1>
          {currentError && showErrors && <Error currentError={currentError} />}

          <form
            action=""
            className="grid grid-cols-12 gap-4 w-full max-w-md "
            noValidate
            onSubmit={handleRegister}
          >
            <div className="col-span-12">
              <input
                type="username"
                id="username"
                name="username"
                placeholder="Username"
                className={`${inputStyles} p-3 rounded-lg w-full mt-2`}
                onChange={handleChange}
              />
            </div>
            <div className="col-span-12">
              <input
                type="email"
                id="email"
                name="email"
                placeholder="Email"
                className={`${inputStyles} p-3 rounded-lg w-full mt-2`}
                onChange={handleChange}
              />
            </div>
            <div className="col-span-12">
              <input
                type="password"
                id="password"
                name="password"
                placeholder="Password"
                className={`${inputStyles} p-3 rounded-lg w-full mt-2`}
                onChange={handleChange}
              />
            </div>
            <div className="col-span-12">
              <input
                type="password"
                id="passwordConfirmation"
                name="passwordConfirmation"
                placeholder="Repeat password"
                className={`${inputStyles} p-3 rounded-lg w-full mt-2`}
                onChange={handleChange}
              />
            </div>
            <div className="col-span-12 flex justify-center p-0 py-2">
              <button
                type="submit"
                className="text-white bg-[#3B98AB] p-2 rounded font-bold w-full"
              >
                Register
              </button>
            </div>
            <div className="col-span-12">
              <p className="text-center font-bold">
                Already have an account? {""}
                <a href="/login" className="text-purple-300 font-bold">
                  Login to your account
                </a>
              </p>
            </div>
          </form>
          <div className="mt-10">{isLoading && <Spinner />}</div>
        </div>
      </div>
    </>
  );
}

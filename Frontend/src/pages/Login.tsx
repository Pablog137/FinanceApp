import "../styles/pages/Login.css";
import Spinner from "../components/UI/Spinner";
import { useDarkMode } from "../context/DarkModeContext";
import useForm from "../hooks/useForm";
import { useState } from "react";
import Error from "../components/UI/Error";
import { setCookie, setItemLocalStorage } from "../helpers/localStorage";

export default function Login() {
  const { textColor, inputStyles } = useDarkMode();
  const { errors, handleChange, handleSubmit, showErrors, values } =
    useForm("login");

  const [isLoading, setIsLoading] = useState(false);
  const [serverError, setServerError] = useState("");

  const handleLogin = async (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();
    const formSubmitted = handleSubmit(e);
    if (!formSubmitted) return;

    setIsLoading(true);

    try {
      const response = await fetch(
        `${import.meta.env.VITE_URL}/api/auth/login`,
        {
          method: "POST",
          headers: {
            "Content-Type": "application/json",
          },
          body: JSON.stringify(values),
        }
      );

      const data = await response.json();
      if (!response.ok) {
        setServerError(data.message);
        setIsLoading(false);
        return;
      }
      setItemLocalStorage("token", data.token);
      setItemLocalStorage("user", JSON.stringify({ email: data.email }));
      setCookie("refresh_token", data.refreshToken);
      // Redirect to the dashboard
    } catch (err: any) {
      setServerError(err.message);
    } finally {
      setIsLoading(false);
    }
  };

  const currentError = errors?.email || errors?.password || serverError;

  return (
    <>
      <div className="bubbles">
        <div className="bubble bubble1"></div>
        <div className="bubble bubble2"></div>
        <div className="bubble bubble6"></div>
      </div>

      <div className="flex justify-center items-center min-h-screen">
        <div
          className={`${textColor} grid place-items-center px-6 login-container p-16`}
        >
          <h1 className="text-3xl md:text-4xl text-center font-bold py-8 md:p-10">
            Sign in
          </h1>
          {currentError && showErrors && <Error currentError={currentError} />}
          <form
            action=""
            onSubmit={handleLogin}
            className="grid grid-cols-12 gap-4 w-full max-w-md "
            noValidate
          >
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
              <div className="flex justify-between">
                <span className="">
                  <a
                    href="/forgotPassword"
                    className="text-purple-300 text-xs font-bold"
                  >
                    Forgot password?
                  </a>
                </span>
              </div>
              <input
                type="password"
                id="password"
                name="password"
                placeholder="Password"
                className={`${inputStyles} p-3 rounded-lg w-full mt-2`}
                onChange={handleChange}
              />
            </div>
            <div className="col-span-12 flex justify-center p-0 py-2">
              <button
                type="submit"
                className="text-white bg-[#3B98AB] p-2 rounded font-bold w-full"
              >
                Sign in
              </button>
            </div>
            <div className="col-span-12">
              <p className="text-center font-bold">
                Don’t have an account? {""}
                <a href="/register" className="text-purple-300 font-bold">
                  Create an account
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

import { useState } from "react";
import "../styles/pages/Login.css";
import Spinner from "../components/UI/Spinner";
import { useDarkMode } from "../context/DarkModeContext";

export default function Login() {
  const [errorMessage, setErrorMessage] = useState("");
  const [serverError, setServerError] = useState("");
  const [showError, setShowError] = useState(false);
  const [isLoading, setIsLoading] = useState(false);
  const [formData, setFormData] = useState({
    email: "",
    password: "",
  });
  const { textColor, inputStyles } = useDarkMode();

  const onChangeInput = (e: React.ChangeEvent<HTMLInputElement>) => {
    setFormData({ ...formData, [e.target.name]: e.target.value });
  };

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
          {errorMessage && showError && (
            <div className="w-full text-red-400 text-sm text-center p-3 mb-5 rounded border border-red-900 bg-red-950">
              {errorMessage}
            </div>
          )}

          {serverError && (
            <div className="w-full text-red-400 text-sm text-center p-3 mb-5 rounded border border-red-900 bg-red-950">
              {serverError}
            </div>
          )}

          <form
            action=""
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
                onChange={onChangeInput}
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
                onChange={onChangeInput}
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

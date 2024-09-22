import { Route, Routes } from "react-router";
import LandingPage from "../pages/LandingPage";
import Login from "../pages/Login";
import Register from "../pages/Register";
import { useDarkMode } from "../context/DarkModeContext";

const ThemedApp = () => {
  const { isDarkMode } = useDarkMode();

  const bgColor = isDarkMode ? "dark" : "white";

  return (
    <div className={bgColor}>
      <Routes>
        <Route path="/" element={<LandingPage />} />
        <Route path="/login" element={<Login />} />
        <Route path="/register" element={<Register />} />
      </Routes>
    </div>
  );
};

export default ThemedApp;

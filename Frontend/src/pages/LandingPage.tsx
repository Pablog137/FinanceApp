import Hero from "../components/LandingPage/Hero";
import Header from "../components/LandingPage/Header";
import { useDarkMode } from "../context/DarkModeContext";

export default function LandingPage() {
  const { isDarkMode, toggleDarkMode, textColor } = useDarkMode();

  return (
    <>
      <Header
        textColor={textColor}
        isDarkMode={isDarkMode}
        toggleDarkMode={toggleDarkMode}
      />
      <Hero textColor={textColor} />
    </>
  );
}

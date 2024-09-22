import Logo from "./Logo";
import NavBar from "./Navbar";

interface HeaderProps {
  textColor: string;
  isDarkMode: boolean;
  toggleDarkMode: () => void;
}

export default function Header({
  textColor,
  isDarkMode,
  toggleDarkMode,
}: HeaderProps) {
  return (
    <section className="grid grid-cols-12 p-6 ">
      <Logo textColor={textColor} />
      <NavBar
        textColor={textColor}
        isDarkMode={isDarkMode}
        toggleDarkMode={toggleDarkMode}
      />
    </section>
  );
}

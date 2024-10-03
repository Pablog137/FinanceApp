import { useState } from "react";
import { links } from "../../helpers/links";
import LinkList from "./LinkList";
import LinkItem from "./LinkItem";
import LightModeIcon from "../UI/Icons/LightModeIcon";
import MenuIcon from "../UI/Icons/MenuIcon";

interface NavBarProps {
  textColor: string;
  isDarkMode: boolean;
  toggleDarkMode: () => void;
}

export default function NavBar({
  textColor,
  isDarkMode,
  toggleDarkMode,
}: NavBarProps) {
  const [menuOpen, setMenuOpen] = useState(false);

  const toggleMenu = () => {
    setMenuOpen(!menuOpen);
  };

  return (
    <nav
      className={`border-gray-200 col-span-6 lg:col-span-9 grid grid-cols-12 items-center justify-end gap-4 ${
        menuOpen ? "mb-10" : "mb-20 md:mb-40"
      }`}
    >
      <ul
        className={`${textColor} hidden lg:flex col-span-6 justify-start gap-12 text-lg mx-5`}
      >
        <LinkList />
      </ul>

      <div
        className={`col-span-12 lg:col-span-6 flex justify-end gap-4 items-center pe-4 ${textColor}`}
      >
        <LinkItem path="/login" text="Sign in" />
        <LinkItem path="/register" text="Sign Up" />
        <LightModeIcon
          isDarkMode={isDarkMode}
          toggleDarkMode={toggleDarkMode}
        />
        <MenuIcon toggleMenu={toggleMenu} textColor={textColor} />
      </div>

      {menuOpen && (
        <div className="col-span-12 mt-4 lg:hidden text-white ">
          <ul className="bg-white list-none p-2 dark:bg-gray-800 border border-gray-200 dark:border-gray-700 rounded-md shadow-md">
            {links.map((link, index) => (
              <li key={index}>
                <a
                  href={link.url}
                  className="block px-4 py-2 text-gray-800 dark:text-white hover:bg-gray-200 dark:hover:bg-gray-700"
                  onClick={toggleMenu}
                >
                  {link.name}
                </a>
              </li>
            ))}
          </ul>
        </div>
      )}
    </nav>
  );
}

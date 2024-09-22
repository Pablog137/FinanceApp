import { Link } from "react-router-dom";

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
  return (
    <nav className="border-gray-200 col-span-8 grid grid-cols-12 items-center justify-end gap-4 ">
      <ul
        className={`${textColor} flex col-span-8 justify-end gap-12 text-lg me-5`}
      >
        <li>
          <a href="Pricing" className="hover:text-green-200">
            Pricing
          </a>
        </li>
        <li>
          <a href="Features" className="hover:text-green-200">
            Features
          </a>
        </li>
      </ul>
      <div className={`col-span-4 flex justify-end gap-4 items-center pe-4 ${textColor}`}>
        <Link to="/login">
          <button className="px-4 py-2 rounded-sm border hover:border-green-300 hover:border-2">
            Sign in
          </button>
        </Link>
        <Link to="/register">
          <button className=" px-4 py-2 rounded-sm border hover:border-green-300 hover:border-2">
            Sign Up
          </button>
        </Link>
        {isDarkMode ? (
          <i
            className="fa-solid fa-sun text-yellow-300 text-3xl ps-4"
            onClick={toggleDarkMode}
          ></i>
        ) : (
          <i
            className="fa-solid fa-moon text-black text-3xl ps-4"
            onClick={toggleDarkMode}
          ></i>
        )}
      </div>
    </nav>
  );
}

import { useDarkMode } from "../../../context/DarkModeContext";
import AsideElement from "./AsideElement";
import AsideElementList from "./AsideElementList";

export default function AsideMobile() {
  const { isDarkMode, textColor } = useDarkMode();

  const bgColor = isDarkMode ? "dark:bg-gray-800" : "bg-white";
  const stylesLightMode = !isDarkMode && "opacity-90";
  const bgHoverColor = isDarkMode ? "hover:bg-gray-700" : "hover:bg-gray-100";

  return (
    <aside
      id="logo-sidebar"
      className={`${bgColor} flex justify-center fixed inset-x-0 bottom-0 w-full transition-transform sm:translate-x-0 flex-shrink-0 overflow-y-auto p-2 lg:hidden ${stylesLightMode}`}
      aria-label="Sidebar"
    >
      <ul className="font-medium list-none flex justify-center items-center">
        <AsideElementList textColor={textColor} />
      </ul>
      <ul className="font-medium flex items-center justify-center list-none">
        <AsideElement
          text="Settings"
          icon="fa-solid fa-gear"
          url="/settings"
          textColor={textColor}
        />
        <li className="p-2 lg:w-full">
          <a
            className={`${textColor} ${bgHoverColor} flex items-center p-2 rounded-lg group cursor-pointer`}
          >
            <i
              className={`fa-solid fa-arrow-right-from-bracket text-xl md:text-2xl transition duration-75 `}
            ></i>
            <span className="flex-1 whitespace-nowrap ms-3 hidden lg:flex">
              Sign out
            </span>
          </a>
        </li>
      </ul>
    </aside>
  );
}

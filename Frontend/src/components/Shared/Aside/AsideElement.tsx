import { useDarkMode } from "../../../context/DarkModeContext";

type Props = {
  text: string;
  icon: string;
  url: string;
  textColor: string;
  textSize?: string;
};

export default function AsideElement({
  text,
  icon,
  url,
  textColor,
  textSize = "text-xl",
}: Props) {
  const { isDarkMode } = useDarkMode();

  const bgHoverColor = isDarkMode ? "hover:bg-gray-700" : "hover:bg-gray-100";

  return (
    <li className="p-2 lg:w-full">
      <a
        href={url}
        className={`
          ${textColor} ${bgHoverColor} flex items-center p-2 rounded-lg group
        `}
      >
        <i
          className={`
            ${icon} ${textSize} md:text-2xl transition duration-75 
          `}
        ></i>
        <span className="flex-1 whitespace-nowrap ms-3 hidden lg:flex">
          {text}
        </span>
      </a>
    </li>
  );
}

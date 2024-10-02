import { useDarkMode } from "../../context/DarkModeContext";
import Card from "./Card";

type Props = {
  colMain: string;
};

export default function Main({ colMain }: Props) {
  const { isDarkMode, textColor } = useDarkMode();

  const bgColor = isDarkMode ? "bg-[#161922]" : "bg-[rgb(246,246,246)]";

  return (
    <>
      <div
        className={`${textColor} pt-20 md:pt-40 h-screen ${colMain} ${bgColor}`}
      >
        <div className="text-white p-10">
          <Card />
        </div>
      </div>
    </>
  );
}

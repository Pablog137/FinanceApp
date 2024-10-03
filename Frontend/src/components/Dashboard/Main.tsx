import { useDarkMode } from "../../context/DarkModeContext";
import Card from "./Card";

type Props = {
  colMain: string;
};

export default function Main({ colMain }: Props) {
  const { textColor } = useDarkMode();

  return (
    <>
      <div
        className={`${textColor} pt-5 md:pt-10 h-screen ${colMain} grid grid-cols-12 p-5`}
      >
        <div className="text-white p-10 col-span-12 ">
          <Card />
        </div>
        {/* Quick actions */}
        <div className="col-span-12">
          <h1 className="text-lg font-semibold">Quick actions</h1>
        </div>
        {/* Schedule payments */}
        <div className="col-span-12">
          <h1 className="text-lg font-semibold">Schedule payments</h1>
        </div>
      </div>
    </>
  );
}

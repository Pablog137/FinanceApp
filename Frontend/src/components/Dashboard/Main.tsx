import { useDarkMode } from "../../context/DarkModeContext";
import Card from "./Card";
import QuickActions from "./QuickActions";
import SchedulePayments from "./SchedulePayments";

type Props = {
  colMain: string;
};

export default function Main({ colMain }: Props) {
  const { textColor } = useDarkMode();

  return (
    <>
      <div
        className={`${textColor} pt-5 md:pt-10 ${colMain} grid grid-cols-12 p-10`}
      >
        <div className="text-white p-10 col-span-12 md:col-span-6 xl:col-span-4">
          <Card />
        </div>
        <div className="p-10 hidden md:block md:col-span-6 xl:col-span-4">
          <h1 className={`${textColor} text-2xl font-semibold`}>Summary</h1>
          <div className="">
            <div className="pt-5 flex flex-col">
              <div className="bg-gray-200 p-6 rounded-md flex justify-between">
                {/* Graph */}
                <div className="flex flex-col gap-2">
                  <h1 className="text-black font-semibold">Total expenses</h1>
                  <div className="flex justify-between gap-4">
                    <div>
                      <p className="text-sm text-gray-400 font-semibold">
                        Last month
                      </p>
                      <p className="text-2xl font-semibold">$ 1,000</p>
                    </div>
                    <div>
                      <p className="text-sm text-gray-400 font-semibold">
                        This month
                      </p>
                      <p className="text-2xl font-semibold">$ 56,000</p>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
          <div className="">
            <div className="pt-5 flex flex-col">
              <div className="bg-gray-200 p-6 rounded-md flex justify-between">
                {/* Graph */}
                <div className="flex flex-col gap-2">
                  <h1 className="text-black font-semibold">Total expenses</h1>
                  <div className="flex justify-between gap-4">
                    <div>
                      <p className="text-sm text-gray-400 font-semibold">
                        Last month
                      </p>
                      <p className="text-2xl font-semibold">$ 1,000</p>
                    </div>
                    <div>
                      <p className="text-sm text-gray-400 font-semibold">
                        This month
                      </p>
                      <p className="text-2xl font-semibold">$ 56,000</p>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
        <QuickActions />
        <SchedulePayments />
      </div>
    </>
  );
}

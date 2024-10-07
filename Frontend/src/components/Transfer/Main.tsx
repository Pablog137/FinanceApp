import { useDarkMode } from "../../context/DarkModeContext";
import NewTransferForm from "./NewTransferForm";
import RecentTransfer from "./RecentTransfer";
import SearchContact from "./SearchContact";

type Props = {
  colMain: string;
};

export default function Main({ colMain }: Props) {
  const { textColor } = useDarkMode();

  return (
    <>
      <div
        className={`${textColor} ${colMain} grid grid-cols-12 p-10 xl:p-20 pb-20 pt-10 md:pt-20`}
      >
        <div className="col-span-12 justify-center ">
          <h1 className="text-xl font-semibold pt-5 pb-10 text-center">
            Money transfer
          </h1>
          <SearchContact />
          <RecentTransfer />
          <NewTransferForm />
        </div>
      </div>
    </>
  );
}

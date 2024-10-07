export default function NewTransferForm() {
  return (
    <div className="flex flex-col justify-center items-center">
      <h1 className="text-lg font-semibold pt-16 pb-5 text-start">
        Make new transfer
      </h1>
      <form action="" className="grid grid-cols-12 gap-4 w-full max-w-md px-1">
        <div className="col-span-12">
          <input
            type="username"
            id="username"
            name="username"
            placeholder="Username"
            className={`p-3 rounded-lg w-full mt-2`}
          />
        </div>
        <div className="col-span-12">
          <input
            type="tel"
            id="tel"
            name="tel"
            placeholder="Receiver's phone number"
            className={`p-3 rounded-lg w-full mt-2`}
          />
        </div>
        <div className="col-span-12">
          <input
            type="password"
            id="password"
            name="password"
            placeholder="Password"
            className={`p-3 rounded-lg w-full mt-2`}
          />
        </div>
        <div className="p-2 col-span-12 grid grid-cols-12">
          <button className="col-start-4 col-end-10 p-4 bg-green-400 rounded-md text-white font-bold">
            Continue
          </button>
        </div>
      </form>
    </div>
  );
}

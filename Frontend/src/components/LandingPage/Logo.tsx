export default function Logo({ textColor }: { textColor: string }) {
  return (
    <header className="col-span-2 md:col-span-4">
      <div className="col-span-12 flex items-center gap-5">
        <img
          src="/images/logo.svg"
          alt="image"
          style={{ width: "40px", height: "40px" }}
        />
        <h1 className={`${textColor} text-3xl  font-bold`}>FinanceApp</h1>
      </div>
    </header>
  );
}
